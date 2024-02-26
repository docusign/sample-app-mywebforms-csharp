import React from 'react';
import { Link } from 'react-router-dom';
import {
  sentStatus,
  signingCompletedEvent,
  signingFocusedViewCompletedEvent,
} from '../constants';

export const getNextSigningStep = (data, event, t) => {
  const { status, redirectUris } = data ?? {};
  if (event === signingCompletedEvent) {
    if (status === sentStatus) {
      return (
        <strong>
          <Link to={redirectUris[1]}>{t('NextSignerLink')}</Link>
        </strong>
      );
    }
    return <strong>{t('CompletedText')}</strong>;
  } if (event === signingFocusedViewCompletedEvent) {
    return <strong>{t('CompletedText')}</strong>;
  }
  return <Link to='/'>{t('HomeLink')}</Link>;
};
