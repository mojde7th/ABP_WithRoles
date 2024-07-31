import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getBooks, deleteBook } from '../services/bookService';

const BookList = () => {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    loadBooks();
  }, []);

  const loadBooks = async () => {
    try {
      const response = await getBooks();
      // Check if response data has 'items' array
      if (response.data && Array.isArray(response.data.items)) {
        setBooks(response.data.items);
      } else {
        console.error('API response does not contain items array', response.data);
        setBooks([]);
      }
    } catch (error) {
      console.error('Error fetching books:', error);
      setBooks([]);
    }
  };

  const handleDelete = (id) => {
    deleteBook(id).then(() => loadBooks());
  };

  return (
    <div>
      <h1>Books</h1>
      <Link to={`/publishers`}>Back to Publishers' list Page</Link>
      <br/>
      <Link to={`/publishers/new`}>Back to adding Publishers</Link>
      <br/>
      <Link to={`/roles/create`}>Back to Creating new Roles Page</Link>
      <br/>
      <Link to={`/roles/assign`}>Back to Assigning Roles to users Page</Link>
      <br/>
      <Link to="/books/new">Back to Adding new Books</Link>
      <br/>
      <ul>
        {books.map(book => (
          <li key={book.id}>
            {book.name} - {book.author} - ${book.price}
            <Link to={`/books/edit/${book.id}`}>Edit</Link>
            <button onClick={() => handleDelete(book.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BookList;
