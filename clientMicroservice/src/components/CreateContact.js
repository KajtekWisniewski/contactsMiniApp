'use client';

import React, { useState } from 'react';
import axios from 'axios';
import styles from '@/app/stockStyles.module.scss';

const ContactForm = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    categoryId: 1,
    subcategoryId: null,
    phone: '',
    dateOfBirth: '',
  });
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    try {
        const userSessionResponse = await axios.get('/api/fetch-access-token');

        if (userSessionResponse.status === 200 && userSessionResponse.data.accessToken) {
          const userAccessToken = userSessionResponse.data.accessToken;

      const response = await axios.post(
        `${process.env.NEXT_PUBLIC_DEMO_BACKEND_URL}/contacts`,
        formData,
        {
          headers: {
            'Content-Type': 'application/json',
             Authorization: 'Bearer ' + userAccessToken
          },
        }
      );

      if (response.status === 201) {
        setSuccess('Contact created successfully!');
        setFormData({
          firstName: '',
          lastName: '',
          email: '',
          password: '',
          categoryId: 1,
          subcategoryId: null,
          phone: '',
          dateOfBirth: '',
        });
      }} else {
        setError('Failed to create contact.');
      }
    } catch (err) {
      setError('Error creating contact.');
      console.error(err);
    }

  };

  return (
    <div className={styles.formContainer}>
      <h2>Create New Contact</h2>
      {error && <div className={styles.error}>{error}</div>}
      {success && <div className={styles.success}>{success}</div>}
      <form onSubmit={handleSubmit}>
        <div className={styles.formGroup}>
          <label>First Name:</label>
          <input
            type="text"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className={styles.formGroup}>
          <label>Last Name:</label>
          <input
            type="text"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
        </div>
        <div className={styles.formGroup}>
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className={styles.formGroup}>
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>
        <div className={styles.formGroup}>
          <label>Category:</label>
          <select
            name="categoryId"
            value={formData.categoryId}
            onChange={handleChange}
            required
          >
            <option value="1">Business</option>
            <option value="2">Private</option>
            <option value="3">Other</option>
          </select>
        </div>
        {formData.categoryId === '1' && (
          <div className={styles.formGroup}>
            <label>Subcategory:</label>
            <select
              name="subcategoryId"
              value={formData.subcategoryId}
              onChange={handleChange}
              required
            >
              <option value="1">Boss</option>
              <option value="2">Customer</option>
            </select>
          </div>
        )}
        {formData.categoryId === '2' && (
          <div className={styles.formGroup}>
            <label>Subcategory:</label>
            <select
              name="subcategoryId"
              value={formData.subcategoryId}
              onChange={handleChange}
              required
            >
              <option value="3">Friend</option>
              <option value="4">Family</option>
            </select>
          </div>
        )}
        {formData.categoryId === '3' && (
          <div className={styles.formGroup}>
            <label>Subcategory:</label>
            <input
              type="text"
              name="subcategoryId"
              value={formData.subcategoryId || ''}
              onChange={handleChange}
              required
            />
          </div>
        )}
        <div className={styles.formGroup}>
          <label>Phone:</label>
          <input
            type="text"
            name="phone"
            value={formData.phone}
            onChange={handleChange}
            required
          />
        </div>
        <div className={styles.formGroup}>
          <label>Date of Birth:</label>
          <input
            type="date"
            name="dateOfBirth"
            value={formData.dateOfBirth}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className={styles.submitButton}>
          Create Contact
        </button>
      </form>
    </div>
  );
};

export default ContactForm;
