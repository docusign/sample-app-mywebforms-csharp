import React from 'react';
import { useTranslation } from 'react-i18next';
import PropTypes from 'prop-types';
import parse from 'html-react-parser';
import moment from 'moment/moment';
import { StatusTableDocument } from './status-table-document';

export const StatusTableElement = ({status}) => {
  const { t } = useTranslation('Status');

  const getName = (recipientSignerNames) => 
  recipientSignerNames.length > 0 
  ? recipientSignerNames.reduce((a, b) => `${a}&${b}`)
  : '';

  const getLastUpdate = () => status.lastUpdate ? moment(status.lastUpdate).fromNow() : 'NaN';

  return (
    <tr key={status.id}>
      <td>
        {status.emailSubject}
      </td>
      <td>
        {getName(status.recipientSignerNames)}
      </td>
      <td>
        {status.status}
      </td>
      <td>
        {getLastUpdate()}
      </td>
      <td>
        <div className='dropdown'>
          <button className='btn btn-light dropdown-toggle' type='button' 
          id='dropdownMenu1' data-bs-toggle='dropdown' 
          aria-haspopup='true' aria-expanded='true'>
          {parse(t('DocumentsDropDown.Title'))}
          <span className='caret'/>
          </button>
          <ul className='dropdown-menu' aria-labelledby='dropdownMenu1'>
          {status.documents.map(document => 
            <StatusTableDocument key={document.id} document={document} envelopeId={status.id}/>
          )}
          <li><hr className='dropdown-divider'/></li>
          <StatusTableDocument 
            document={{id: 'combined', name: parse(t('DocumentsDropDown.Combined'))}} envelopeId={status.id}/>
          </ul>
        </div>
      </td>
    </tr>
  );
};


StatusTableElement.propTypes = {
  status: PropTypes.shape({
    id: PropTypes.string,
    emailSubject: PropTypes.string,
    status: PropTypes.string,
    recipientSignerNames: PropTypes.arrayOf(PropTypes.string),
    lastUpdate: PropTypes.string,
    documents: PropTypes.arrayOf(PropTypes.shape({
      id: PropTypes.string,
      name: PropTypes.string,
    })),
  })
  };

StatusTableElement.defaultProps = {
  status: {}
};