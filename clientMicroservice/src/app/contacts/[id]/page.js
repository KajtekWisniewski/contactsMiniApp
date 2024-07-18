'use client';

import { useSession } from 'next-auth/react';
import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import styles from '../stockStyles.module.scss';
import PrivateRoute from '@/components/keycloak/PrivateRoute';
import FetchSingleContact from '@/components/FetchSingleContact';

export default function SingleContactPage({params}) {
  const { data: session, status } = useSession();
  const router = useRouter();
  const [isUser, setIsUser] = useState(false);
  const [showInfo, setShowInfo] = useState(false);

  const { id } = params;


  return (
    <div className={styles.mainPage}>
      <FetchSingleContact id={id}></FetchSingleContact>
    </div>
  );
}
