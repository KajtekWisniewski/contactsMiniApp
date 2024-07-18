// contacts/[id]/page.js

'use client';

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useRouter } from 'next/navigation';
import { ClipLoader } from 'react-spinners';
import styles from '@/app/stockStyles.module.scss';
 
const ContactDetail = ({params}) => {
  const router = useRouter();
  const { id } = params;
  const [contact, setContact] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [isUser, setIsUser] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState({});

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

    const fetchContact = async () => {
      setLoading(true);
      setError('');

      try {
        const response = await axios.get(
          `${process.env.NEXT_PUBLIC_DEMO_BACKEND_URL}/contacts/${id}`,
        );

        setContact(response.data);
        setFormData(response.data); // Initialize form data with fetched contact
      } catch (err) {
        setError('Error fetching contact data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    if (id) {
      checkUserStatus();
      fetchContact();
    }
  }, [id]);

  const handleDelete = async () => {
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

        router.push('/contacts'); // Redirect to contacts list after deletion
      } else {
        setError('You do not have permission to delete this contact.');
      }
    } catch (err) {
      setError('Error deleting contact data');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleEdit = () => {
    setIsEditing(true);
  };

  const handleSave = async () => {
    setLoading(true);
    setError('');

    try {
      const userSessionResponse = await axios.get('/api/fetch-access-token');

      if (userSessionResponse.status === 200 && userSessionResponse.data.accessToken) {
        const userAccessToken = userSessionResponse.data.accessToken;

        await axios.put(
          `${process.env.NEXT_PUBLIC_DEMO_BACKEND_URL}/contacts/${id}`,
          formData,
          {
            headers: {
              'Content-Type': 'application/json',
              Authorization: 'Bearer ' + userAccessToken
            }
          }
        );

        setIsEditing(false);
        setContact(formData); // Update contact with new data
      } else {
        setError('You do not have permission to edit this contact.');
      }
    } catch (err) {
      setError('Error updating contact data');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  return (
    <div className={styles.mainPage}>
      {loading && (
        <div>
          <ClipLoader
            color="#256168"
            size={30}
            aria-label="Loading Spinner"
            data-testid="loader"
          />
        </div>
      )}
      {contact && (
        <>
          <h1>Contact Details</h1>
          {isEditing ? (
            <div>
              <input
                type="text"
                name="firstName"
                value={formData.firstName}
                onChange={handleChange}
              />
              <input
                type="text"
                name="lastName"
                value={formData.lastName}
                onChange={handleChange}
              />
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleChange}
              />
              <input
                type="text"
                name="phone"
                value={formData.phone}
                onChange={handleChange}
              />
              <input
                type="text"
                name="dateOfBirth"
                value={formData.dateOfBirth}
                onChange={handleChange}
              />
              <select
                name="category"
                value={formData.category}
                onChange={handleChange}
              >
                <option value="1">Business</option>
                <option value="2">Private</option>
                <option value="3">Other</option>
              </select>
              {formData.category === '1' && (
                <select
                  name="subcategory"
                  value={formData.subcategory}
                  onChange={handleChange}
                >
                  <option value="1">Boss</option>
                  <option value="2">Customer</option>
                </select>
              )}
              {formData.category === '2' && (
                <select
                  name="subcategory"
                  value={formData.subcategory}
                  onChange={handleChange}
                >
                  <option value="3">Friend</option>
                  <option value="4">Family</option>
                </select>
              )}
              {formData.category === '3' && (
                <input
                  type="text"
                  name="subcategory"
                  value={formData.subcategory}
                  onChange={handleChange}
                />
              )}
              <button onClick={handleSave} disabled={loading}>
                Save
              </button>
            </div>
          ) : (
            <div>
              <p><strong>First Name:</strong> {contact.firstName}</p>
              <p><strong>Last Name:</strong> {contact.lastName}</p>
              <p><strong>Email:</strong> {contact.email}</p>
              <p><strong>Phone:</strong> {contact.phone}</p>
              <p><strong>Date of Birth:</strong> {contact.dateOfBirth}</p>
              <p><strong>Category:</strong> {contact.category}</p>
              <p><strong>Subcategory:</strong> {contact.subcategory}</p>
              {isUser && (
                <>
                  <button onClick={handleEdit} disabled={loading}>
                    Edit
                  </button>
                  <button onClick={handleDelete} disabled={loading}>
                    Delete
                  </button>
                </>
              )}
            </div>
          )}
        </>
      )}
      {error && <div style={{ color: 'red' }}>{error}</div>}
    </div>
  );
};

export default ContactDetail;
