import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getPublishers, deletePublisher } from '../services/publisherService';

const PublisherList = () => {
  const [publishers, setPublishers] = useState([]);

  useEffect(() => {
    loadPublishers();
  }, []);

  const loadPublishers = () => {
    getPublishers().then(response => setPublishers(response.data));
  };

  const handleDelete = (id) => {
    deletePublisher(id).then(() => loadPublishers());
  };

  return (
    <div>
      <h2>Publishers List</h2>
      <Link to="/publishers/new">Back to Adding Publishers page</Link>
      <br/>
      <Link to={`/books`}>Back to Book List</Link>
      <br/>
      {/* <Link to={`/publishers`}>Back to Publishers' list Page</Link> */}
     
      <Link to={`/roles/create`}>Back to Creating new Roles Page</Link>
      <br/>
      <Link to={`/roles/assign`}>Back to Assigning Roles to users Page</Link>
      <br/>
      <Link to="/books/new">Back to Addong new Books</Link>
      <br/>
      <ul>
        {publishers.map(publisher => (
          <li key={publisher.id}>
            {publisher.name}
            <Link to={`/publishers/edit/${publisher.id}`}>Edit</Link>
            <button onClick={() => handleDelete(publisher.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PublisherList;
