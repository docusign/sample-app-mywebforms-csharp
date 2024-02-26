import React from 'react';
import { useTranslation } from 'react-i18next';

export const Header = () => {
  const { t } = useTranslation('Common');

  return (
    <nav className='navbar navbar-expand-md navbar-dark bg-primary'>
      <div className='container-md'>
        <a className='navbar-brand' href='/'>
          {t('ApplicationName')}
          <div>{t('SecondaryName')}</div>
        </a>
        <button
          className='navbar-toggler'
          type='button'
          data-bs-toggle='collapse'
          data-bs-target='#navbarSupportedContent'
          aria-controls='navbarSupportedContent'
          aria-expanded='false'
          aria-label='Toggle navigation'
        >
          <span className='navbar-toggler-icon' />
        </button>
        <div className='collapse navbar-collapse' id='navbarSupportedContent'>
          <ul className='navbar-nav me-auto mb-2 mb-md-0' />
          <form className='d-flex'>
            <ul className='navbar-nav me-auto mb-2 mb-md-0'>
              <li className='nav-item'>
                <a
                  className='nav-link'
                  href='https://github.com/docusign/sample-app-mywebforms-csharp'
                  target='_blank'
                  rel='noreferrer'
                >
                  {t('GitHubLink')}
                </a>
              </li>
              <li className='nav-item'>
                <a className='nav-link' href='/status'>
                  {t('StatusLink')}
                </a>
              </li>
              <li className='nav-item'>
                <a className='nav-link' href='./about-us'>
                  {t('AboutLink')}
                </a>
              </li>
            </ul>
          </form>
        </div>
      </div>
    </nav>
  );
};
