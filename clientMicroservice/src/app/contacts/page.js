'use client';

import { useSession } from 'next-auth/react';
import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import styles from '../stockStyles.module.scss';
import PrivateRoute from '@/components/keycloak/PrivateRoute';
import FetchContacts from '@/components/FetchContacts';

export default function ContactsPage() {
  const { data: session, status } = useSession();
  const router = useRouter();
  const [isUser, setIsUser] = useState(false);
  const [showInfo, setShowInfo] = useState(false);


  return (
    <div className={styles.mainPage}>
      <FetchContacts></FetchContacts>
    </div>
  );
}
