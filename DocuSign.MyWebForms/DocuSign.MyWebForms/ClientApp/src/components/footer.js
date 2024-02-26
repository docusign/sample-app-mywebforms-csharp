import React from 'react';
import { useTranslation } from 'react-i18next';

export const Footer = () => {
  const { t } = useTranslation('Common');
  return (
    <div className='container'>
      <footer role='contentinfo' className='footer'>
        {t('Copyright')}
      </footer>
    </div>
  );
};
