import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import parse from 'html-react-parser';
import { StatusTable } from './components';
import { useLazyGetStatusesQuery } from '../../api';
import { Loader } from '../../components';

export const Status = () => {
  const { t } = useTranslation('Status');

  const [statuses, setStatuses] = useState(undefined);

  const [getStatusesQuery] = useLazyGetStatusesQuery();

  const receiveStatuses = async () => {
    const receivedStatuses = await getStatusesQuery().unwrap();
    setStatuses(receivedStatuses);
  };

  useEffect(() => {
    receiveStatuses();
  }, []);

  const getLoader = () => (
    <div className='loader'>
      <Loader />
    </div>
  );
  const getEmptyElements = () => (
    <h3 className='no-items-title text-center'>{parse(t('NoItemsHeader'))}</h3>
  );
  const getMultipleElements = () => (
    <div>
      <h3 className='main-title'>{parse(t('Header1'))}</h3>

      <p className='h3-hint'>
        {parse(t('Header2'))}(
        <a href='/api/account/logout'>{parse(t('RestartSession'))}</a>)
      </p>

      <StatusTable statuses={statuses} />
    </div>
  );

  const getElement = () => {
    let element;
    if (statuses === undefined) {
      element = getLoader();
    } else if (statuses.length === 0) {
      element = getEmptyElements();
    } else {
      element = getMultipleElements();
    }
    return element;
  };

  return (
    <div className='status-page'>
      <div className='container'>{getElement()}</div>
    </div>
  );
};
