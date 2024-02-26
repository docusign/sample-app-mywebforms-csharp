import React, { Suspense, useEffect } from 'react';
import './assets/scss/main.scss';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './components';
import { Home } from './pages/home';
import { useLazyIsAuthenticatedQuery, useLazyLoginQuery } from './api';
import { About } from './pages/about/about';
import { PersonalLoan } from './pages/personalLoan';
import { AutoLoan } from './pages/autoLoan';
import { SailboatLoan } from './pages/sailboatLoan';
import { SignCompleted } from './pages/signCompleted';
import { Status } from './pages/status';

const routes = (
  <Routes>
    <Route path='/' element={<Home />} />
    <Route
      path='/sign/completed'
      element={
        <Suspense>
          <SignCompleted />
        </Suspense>
      }
    />
    <Route path='/about-us' element={<About />} />
    <Route
      path='/status'
      element={
        <Suspense>
          <Status />
        </Suspense>
      }
    />
    <Route
      path='/loan/personal'
      element={
        <Suspense>
          <PersonalLoan />
        </Suspense>
      }
    />
    <Route
      path='/loan/auto'
      element={
        <Suspense>
          <AutoLoan />
        </Suspense>
      }
    />
    <Route
      path='/loan/sailboat'
      element={
        <Suspense>
          <SailboatLoan />
        </Suspense>
      }
    />
  </Routes>
);
const App = () => {
  const [loginQuery] = useLazyLoginQuery();
  const [isAuthenticatedQuery] = useLazyIsAuthenticatedQuery();

  const handleLogin = async () => {
    const isAuthenticated = await isAuthenticatedQuery().unwrap();
    if (!isAuthenticated) {
      await loginQuery();
    }
  };

  useEffect(() => {
    handleLogin();
  }, []);

  return <Layout>{routes}</Layout>;
};

export default App;
