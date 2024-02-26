import React from 'react';
import { useTranslation } from 'react-i18next';
import PropTypes from 'prop-types';
import parse from 'html-react-parser';
import { StatusTableElement } from './status-table-element';

export const StatusTable = ({statuses}) => {
  const { t } = useTranslation('Status');

  return (
    <table className='table table-minimal'>
      <thead>
        <tr>
          <th>
            {parse(t('Table.Column1Name'))}
          </th>
          <th>
            {parse(t('Table.Column2Name'))}
          </th>
          <th>
            {parse(t('Table.Column3Name'))}
          </th>
          <th>
            {parse(t('Table.Column4Name'))}
          </th>
          <th/>
        </tr>
      </thead>
      <tbody>
          {statuses.map(status =>
            <StatusTableElement key={status.id} status={status}/>
          )}
      </tbody>
    </table>
  );
};


StatusTable.propTypes = {
  statuses: PropTypes.arrayOf(PropTypes.shape({
    id: PropTypes.string,
    emailSubject: PropTypes.string,
    recipientSignerNames: PropTypes.arrayOf(PropTypes.string),
    lastUpdate: PropTypes.string,
    documents: PropTypes.arrayOf(PropTypes.shape({
      id: PropTypes.string,
      name: PropTypes.string,
    })),
  }))
  };
  
StatusTable.defaultProps = {
  statuses: []
};