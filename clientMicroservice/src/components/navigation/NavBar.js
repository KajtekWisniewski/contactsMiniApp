'use client';
import Link from 'next/link';
import styles from './NavBar.module.scss';
import Image from 'next/image';
import AuthStatus from '../keycloak/authStatus';
import { useSession } from 'next-auth/react';
import React, { useState, useEffect } from 'react';

const NavBar = ({}) => {
  const { data: session, status } = useSession();

  return (
    <>
      <nav className={styles.navbar}>

        <Link className={styles.linkStyle} href={`/contacts/`}>
              <h2>contacts list</h2>
        </Link>
        <Link className={styles.linkStyle} href={`/`}>
          <Image src="/chart_icon.png" width={75} height={75} alt="site logo" />
        </Link>
        <AuthStatus></AuthStatus>
      </nav>
    </>
  );
};

export default NavBar;
