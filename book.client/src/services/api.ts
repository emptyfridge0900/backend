import { Book } from '../types/book';

const BASE_URL = '/api'; 

// GET /api/books
export const fetchBooks = async (
  pageIndex: number,
  itemsPerPage: number,
  author?: string,
  title?: string
): Promise<{ totalRecords: number; itemPage: number; pageIndex: number; results: Book[] }> => {
  const params = new URLSearchParams({
    pageIndex: pageIndex.toString(),
    itemPage: itemsPerPage.toString(),
    ...(author && { author }),
    ...(title && { title }),
  });
  const response = await fetch(`${BASE_URL}/books?${params}`);
  if (!response.ok) throw new Error('Failed to fetch books');
  return response.json();
};

// GET /api/books/:id
export const fetchBookById = async (id: number): Promise<Book> => {
  const response = await fetch(`${BASE_URL}/books/${id}`);
  if (!response.ok) throw new Error('Book not found');
  return response.json();
};


// POST /api/books
export const addBook = async (
  book: Omit<Book, 'id' | 'quantity' | 'totalSales'>
): Promise<{ 
  id: number;
  title:string;
  author:string
  quantity: number;
  totalSales: number; }> => {
  const response = await fetch(`${BASE_URL}/books`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(book),
  });
  if (!response.ok) throw new Error('Failed to add book');
  return response.json(); // Expecting { Results: [newBook] }
};

// PUT /api/books/:id
export const updateBook = async (
  id: number,
  book: Omit<Book, 'id' >
): Promise<number> => {
  console.log(book)
  const response = await fetch(`${BASE_URL}/books/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(book),
  });
  if (!response.ok) throw new Error('Failed to update book');
  return response.json(); // Returns changedNumber
};

// DELETE /api/books/:id
export const deleteBook = async (id: number): Promise<void> => {
  const response = await fetch(`${BASE_URL}/books/${id}`, {
    method: 'DELETE',
  });
  if (!response.ok) throw new Error('Failed to delete book');
};