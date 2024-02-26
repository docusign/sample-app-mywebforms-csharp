import React, { useEffect } from 'react';
import parse from 'html-react-parser';
import { useTranslation } from 'react-i18next';
import { Link, useLocation } from 'react-router-dom';
import { useUpdateStatusMutation } from '../../api';
import { Loader } from '../../components';
import { getNextSigningStep } from '../../utils/getNextSigningStep';
import { signingFocusedViewCompletedEvent } from '../../constants';

export const SignCompleted = () => {
  const { t } = useTranslation('SignCompleted');
  const location = useLocation();
  const params = new URLSearchParams(location.search);
  const event = params.get('event');
  const [updateStatus, { data }] = useUpdateStatusMutation();

  const eventsText = t('EventsText', { returnObjects: true });

  useEffect(() => {
    if (event !== signingFocusedViewCompletedEvent) {
      updateStatus({
        existingEnvelope: true,
      });
    }
  }, []);

  return (
    <div className='signCompleted-page'>
      <div className='container'>
        <div className='row'>
          <div className='col-md-6'>
            {event === signingFocusedViewCompletedEvent || data ? (
              <>
                <h2 className='text-center signingCompleted-title'>
                  {eventsText[event].Title}
                </h2>
                <p className='mb-3'>{eventsText[event].SubText}</p>
                <div>
                  <div className='mb-3'>{getNextSigningStep(data, event, t)}</div>
                  <div className='mb-5'>
                    <Link to='/status'>{t('StatusLink')}</Link>
                  </div>
                </div>
                <section className='action-section'>
                  <div className='text-center action-section'>
                    <div className='row'>
                      <div className='mb-3'>
                        <h4>{t('ActionTitle')}</h4>
                      </div>
                    </div>
                    <div className='row'>
                      <div className='col-md-6 offset-xxl-3 mb-3'>
                        <Link
                          className='btn btn-lg btn-default'
                          to='https://go.docusign.com/o/sandbox'
                          target='_blank'
                          rel='noreferrer'
                        >
                          {t('ActionButton')}
                        </Link>
                      </div>
                    </div>
                    <div className='row'>
                      <div className='col-md-12'>{t('ActionText')}</div>
                    </div>
                  </div>
                </section>
              </>
            ) : (
              <div className='signingCompleted-title'>
                <Loader />
              </div>
            )}
          </div>
          <div className='col-sm-6 mt-4'>{parse(t('InfoText'))}</div>
        </div>
      </div>
    </div>
  );
};
