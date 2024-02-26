import React from 'react';
import PropTypes from 'prop-types';
import { Spinner } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';

export const Loader = ({ message }) => {
  const { t } = useTranslation('Common');
  return (
    <div className='d-flex gap-3 justify-content-center'>
      <Spinner animation='border' role='status' className='spinner' />
      <div className='d-flex align-items-center spinner-text'>
        <p className='m-0'>{message ?? t('LoaderText')}</p>
      </div>
    </div>
  );
};

Loader.defaultProps = {
  message: null
};

Loader.propTypes = {
  message: PropTypes.string,
};
