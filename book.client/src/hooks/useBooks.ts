import { useState, useEffect, useCallback } from 'react';
import { Book } from '../types/book';
import { fetchBooks } from '../services/api';

export const useBooks = (itemsPerPage: number = 10) => {
  const [books, setBooks] = useState<Book[]>([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [pageIndex, setPageIndex] = useState(0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [searchParams, setSearchParams] = useState<{ author?: string; title?: string }>({});

  const loadBooks = useCallback(async () => {
    setLoading(true);
    console.log('loadBooks called with:', { pageIndex, itemsPerPage, searchParams });
    try {
      const data = await fetchBooks(pageIndex, itemsPerPage, searchParams.author, searchParams.title);
      setBooks(data.results || []);
      const newTotalRecords = data.totalRecords ?? 0;
      setTotalRecords(newTotalRecords);
    } catch (err) {
      setError('Failed to load books');
      setTotalRecords(0);
    } finally {
      setLoading(false);
    }
  }, [pageIndex, itemsPerPage, searchParams]);

  const addBookLocally = useCallback((newBook: Book) => {
    // Only add if on the first page or no search filters (to keep pagination simple)
    if (pageIndex === 0 && !searchParams.author && !searchParams.title) {
      setBooks((prevBooks) => {
        const updatedBooks = [...prevBooks, newBook];
        // Trim to itemsPerPage if exceeds (simulates server-side pagination)
        if (updatedBooks.length > itemsPerPage) {
          return updatedBooks.slice(0, itemsPerPage);
        }
        return updatedBooks;
      });
      setTotalRecords((prevTotal) => prevTotal + 1);
    } else {
      // If not on page 1 or filtered, reset to page 1 and refetch
      setPageIndex(0);
      loadBooks();
    }
  }, [pageIndex, searchParams, itemsPerPage, loadBooks]);

  const removeBookLocally = useCallback((bookId: number) => {
    setBooks((prevBooks) => {
      const updatedBooks = prevBooks.filter((book) => book.id !== bookId);
      // If the current page is now empty and not the first page, go to the previous page
      if (updatedBooks.length === 0 && pageIndex > 1) {
        setPageIndex((prevPage) => prevPage - 1);
        loadBooks(); // Fetch the previous page
        return updatedBooks;
      }
      return updatedBooks;
    });
    setTotalRecords((prevTotal) => Math.max(0, prevTotal - 1)); // Ensure totalRecords doesn't go negative
  }, [pageIndex, itemsPerPage, loadBooks]);

  useEffect(() => {
    loadBooks();
  }, [loadBooks]);

  console.log('useBooks returning:', { totalRecords, books, loading, error });
  return {
    books,
    totalRecords,
    pageIndex,
    setPageIndex,
    loading,
    error,
    setSearchParams,
    itemsPerPage,
    addBookLocally, 
    removeBookLocally
  };
};

