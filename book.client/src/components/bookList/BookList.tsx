import React, { useState, useCallback } from 'react';
import { Book } from '../../types/book';
import AddBookModal from './AddBookModal';
import DualSearchBar from '../common/DualSearchBar';
import Pagination from '../common/Pagination';
import { addBook, deleteBook } from '../../services/api';
import { useBooks } from '../../hooks/useBooks';

interface BookListProps {
  onBookAdded?: (book: Book) => void;
}

export const BookList: React.FC<BookListProps> = ({ onBookAdded }) => {
  const ITEMS_PER_PAGE = 10;
  const { books, totalRecords, pageIndex, setPageIndex, loading, error, setSearchParams, addBookLocally, removeBookLocally } = useBooks(ITEMS_PER_PAGE);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [authorSearch, setAuthorSearch] = useState('');
  const [titleSearch, setTitleSearch] = useState('');

  const handleAddBook = useCallback(async (newBook: Omit<Book, 'id' | 'quantity' | 'totalSales'>) => {
    try {
      const response = await addBook(newBook);
      if (response && response.id> 0) {
        const addedBook = response;
        addBookLocally(addedBook); // Add to local state immediately
        if (onBookAdded) {
          onBookAdded(addedBook);
        }
      }
    } catch (error) {
      console.error('Failed to add book:', error);
    }
    setIsModalOpen(false);
  }, [addBookLocally, onBookAdded]);

  

  const handleDeleteBook = useCallback(async (id: number) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await deleteBook(id);
        removeBookLocally(id); // Remove from local state immediately
      } catch (error) {
        console.error('Failed to delete book:', error);
        setPageIndex(0); // Fallback to refetch on error
      }
    }
  }, [removeBookLocally, setPageIndex]);


  const handleSearch = useCallback((author: string, title: string) => {
    setSearchParams({ author, title });
    setPageIndex(0);
  }, [setSearchParams, setPageIndex]);

  const handleBookClick = useCallback((bookId: number) => {
    window.location.href = `/book/${bookId}`;
  }, []);

  if (loading) return <div className="text-center p-5">Loading...</div>;
  if (error) return <div className="text-center p-5 text-red-500">{error}</div>;

  return (
    <div className="p-5 max-w-[1200px] mx-auto">
      <div className="flex flex-col md:flex-row justify-between items-center mb-5 gap-4">
        <DualSearchBar
          authorSearch={authorSearch}
          titleSearch={titleSearch}
          setAuthorSearch={setAuthorSearch}
          setTitleSearch={setTitleSearch}
          onSearch={handleSearch}
        />
        <button 
          className="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600 transition-colors"
          onClick={() => setIsModalOpen(true)}
        >
          Add Book
        </button>
      </div>

      <table className="w-full border-collapse mb-5">
        <thead>
          <tr>
            <th className="p-3 text-left border-b border-gray-200">Title</th>
            <th className="p-3 text-left border-b border-gray-200">Author</th>
            <th className="p-3 text-left border-b border-gray-200">Quantity</th>
            <th className="p-3 text-left border-b border-gray-200">Total Sales</th>
            <th className="p-3 text-left border-b border-gray-200">Actions</th>
          </tr>
        </thead>
        <tbody>
          {books.map((book) => (
            <tr key={book.id}>
              <td 
                className="p-3 border-b border-gray-200 text-blue-600 cursor-pointer hover:underline"
                onClick={() => handleBookClick(book.id)}
              >
                {book.title}
              </td>
              <td className="p-3 border-b border-gray-200">{book.author}</td>
              <td className="p-3 border-b border-gray-200">{book.quantity}</td>
              <td className="p-3 border-b border-gray-200">{book.totalSales}</td>
              <td className="p-3 border-b border-gray-200">
                <button
                  className="px-2 py-1 bg-red-500 text-white rounded hover:bg-red-600"
                  onClick={() => handleDeleteBook(book.id)}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {totalRecords > 0 && (
        <Pagination
          currentPage={pageIndex}
          totalRecords={totalRecords}
          itemsPerPage={ITEMS_PER_PAGE}
          onPageChange={setPageIndex}
        />
      )}

      {isModalOpen && (
        <AddBookModal
          onClose={() => setIsModalOpen(false)}
          onAddBook={handleAddBook}
        />
      )}
    </div>
  );
};

export default BookList;