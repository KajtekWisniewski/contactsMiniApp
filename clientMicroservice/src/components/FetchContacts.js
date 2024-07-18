'use client';

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { ClipLoader } from 'react-spinners';

const FetchContacts = () => {
  const [contacts, setContacts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [isUser, setIsUser] = useState(false);

  useEffect(() => {
    const checkUserStatus = async () => {
      try {
        const sessionResponse = await axios.get('/api/fetch-access-token');
        if (sessionResponse.status === 200 && sessionResponse.data.accessToken) {
          setIsUser(true);
        } else {
          setIsUser(false);
        }
      } catch (err) {
        console.error('Error checking logged in status', err);
      }
    };

    const fetchStocks = async () => {
      setLoading(true);
      setError('');

      try {
        const response = await axios.get(
          `${process.env.NEXT_PUBLIC_DEMO_BACKEND_URL}/contacts`,
        );

        setContacts(response.data); // Adjust based on your API response structure
      } catch (err) {
        setError('Error fetching stocks data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    checkUserStatus();
    fetchStocks();
  }, []);

  const handleDelete = async (id) => {
    setLoading(true);
    setError('');

    try {
      const userSessionResponse = await axios.get('/api/fetch-access-token');

      if (userSessionResponse.status === 200 && userSessionResponse.data.accessToken) {
        const userAccessToken = userSessionResponse.data.accessToken;

        await axios.delete(
          `${process.env.NEXT_PUBLIC_DEMO_BACKEND_URL}/contacts/${id}`,
          {
            headers: {
              'Content-Type': 'application/json',
              Authorization: 'Bearer ' + userAccessToken
            }
          }
        );

        setContacts(contacts.filter(contact => contact.id !== id));
      } else {
        setError('You do not have permission to delete this content.');
      }
    } catch (err) {
      setError('Error deleting stock data');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      {loading &&  <div>
        <ClipLoader
          color="#256168"
          size={30}
          aria-label="Loading Spinner"
          data-testid="loader"
        />
      </div>}
      {contacts.length > 0 && (
        <>
          <h1>Stocks</h1>
          <table>
            <thead>
              <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Email</th>
                {isUser && <th>Quick Delete</th>}
              </tr>
            </thead>
            <tbody>
              {contacts.map((contact) => (
                <tr key={contact.id}>
                  <td>{contact.id}</td>
                  <td>{contact.FirstName}</td>
                  <td>{contact.LastName}</td>
                  <td>{contact.Email}</td>
                  {isUser && <td>
                    <button onClick={() => handleDelete(contact.id)} disabled={loading}>
                      Delete
                    </button>
                  </td>}
                </tr>
              ))}
            </tbody>
          </table>
        </>
      )}
      {error && <div style={{ color: 'red' }}>{error}</div>}
    </div>
  );
};

export default FetchContacts;
