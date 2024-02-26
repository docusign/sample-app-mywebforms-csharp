import React from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

export const ActionSection = () => {
  const { t } = useTranslation('Home');

  return (
    <div className='text-center'>
      <div className='row'>
        <div className='col-md-6 offset-md-3  mb-3'>
          <h3>{t('Footer1')}</h3>
        </div>
      </div>
      <div className='row'>
        <div className='col-xxl-3 offset-xxl-3 mb-1'>
          <Link
            className='btn btn-lg btn-default d-grid'
            to='https://go.docusign.com/o/sandbox'
            target='_blank'
            rel='noreferrer'
          >
            {t('SandBoxButton')}
          </Link>
        </div>
        <div className='col-xxl-3 mb-3'>
          <Link
            className='btn btn-lg btn-default d-grid'
            to='https://developers.docusign.com/'
            target='_blank'
            rel='noreferrer'
          >
            {t('LearnMoreButton')}
          </Link>
        </div>
      </div>
      <div className='row'>
        <div className='col-md-6 offset-md-3'>
          <p>
            {t('Footer2')}
          </p>
        </div>
      </div>
    </div>
  );
};
