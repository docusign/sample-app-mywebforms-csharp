import React from 'react';
import { useTranslation } from 'react-i18next';
import PropTypes from 'prop-types';
import parse from 'html-react-parser';

export const StatusTableDocument = ({document, envelopeId}) => {
  const { t } = useTranslation('Status');
  const generateLink = () => 
  `api/document?documentId=${document.id}&envelopeId=${envelopeId}&documentName=${document.name}`;

  return (
    <li>
      <a className='dropdown-item' 
      href={generateLink()}
      target='_blank'
      rel='noreferrer'>{parse(t('DocumentsDropDown.Download'))} {document.name}</a>
    </li>
  );
};


StatusTableDocument.propTypes = {
  document: PropTypes.shape({
    id: PropTypes.string,
    name: PropTypes.string,
  }),
  envelopeId: PropTypes.string
  };
  
StatusTableDocument.defaultProps = {
  document: {},
  envelopeId: PropTypes.string
};