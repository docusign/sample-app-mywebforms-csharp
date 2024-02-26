import React from 'react';
import { useTranslation } from 'react-i18next';

export const GeneralSection = () => {
  const { t } = useTranslation('Home');

  return (
    <div className='general-section'>
      <div className='row'>
        <div className='col-md-12'>
          <h1 className='pb-2 mt-4 mb-2 main-page-header text-center'>{t('Header1')}</h1>
        </div>
      </div>
      <div className='row'>
        <div className='col-lg-12'>
          <h4 className='text-center'>{t('Header2')}</h4>
        </div>
      </div>
    </div>
  );
};
