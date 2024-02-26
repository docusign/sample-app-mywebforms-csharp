import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import { faSquarePlus, faSquareMinus } from '@fortawesome/free-regular-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import parse from 'html-react-parser';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import { Loader } from './loader';
import { useUpdateStatusMutation, useEmbedFormQuery } from '../api';
import { handleDocuSignLoad } from '../utils/handleDocuSignLoad';

export const Loan = ({ ind }) => {
  const { data, isLoading } = useEmbedFormQuery(ind);
  const navigate = useNavigate();
  const [updateStatus, { isLoading: isMutationLoading }] = useUpdateStatusMutation();
  const { t } = useTranslation('Loan');
  const { t: tCards } = useTranslation('Cards');
  const features = tCards('Cards', { returnObjects: true })[ind].Features;
  const title = t('LoanTitles', { returnObjects: true })[ind];
  const steps = t('LoanSteps', { returnObjects: true })[ind];

  useEffect(() => {
    if (!isLoading) {
      handleDocuSignLoad(data, updateStatus, navigate, ind);
    }
  }, [isLoading]);

  return (
    <div className='loan-page' style={{ minHeight: '1100px' }}>
      <div className='container'>
        <div className='row'>
          <div className='col-sm-6'>
            <h2 className='text-center loan-title'>{title}</h2>
            <hr />
            {isLoading || isMutationLoading ? (
              <Loader />
            ) : (
              <div id='docusign' className='embeddedForm' />
            )}
          </div>
          <div className='col-sm-6'>
            <h2 className='text-center loan-title' style={{ opacity: 0 }}>
              {title}
            </h2>
            <hr />
            <p>
              <span
                style={{ cursor: 'pointer' }}
                data-bs-toggle='collapse'
                data-bs-target='#collapseHow'
                aria-expanded='false'
                aria-controls='collapseHow'
              >
                <FontAwesomeIcon icon={faSquarePlus} />
                <FontAwesomeIcon icon={faSquareMinus} /> {t('GeneralCollapseTitle')}
              </span>
            </p>
            <div id='collapseHow' className='collapse'>
              <hr />
              <h4>{t('FeaturesTitle')}</h4>
              <ul className='mb-5'>
                {features.map((feature) => {
                  return <li key={feature.Text}>{feature.Text}</li>;
                })}
              </ul>
              <h4>{t('CodeFlowTitle')}</h4>
              {steps.map((step, index) => {
                return (
                  <div key={step.Title}>
                    <h5>{step.Title}</h5>
                    {parse(step.Text)}
                    {step.Json && (
                      <div>
                        <p style={{ padding: '20px 0' }}>
                          <span
                            style={{ cursor: 'pointer' }}
                            data-bs-toggle='collapse'
                            data-bs-target={`#collapseJSON${index}`}
                            aria-expanded='false'
                            aria-controls={`collapseJSON${index}`}
                          >
                            <FontAwesomeIcon icon={faSquarePlus} />
                            <FontAwesomeIcon icon={faSquareMinus} />{' '}
                            {t('JsonCollapseTitle')}{' '}
                            {step.AdditionalCollapseTitle &&
                              parse(step.AdditionalCollapseTitle)}
                          </span>
                        </p>
                        <div className='collapse' id={`collapseJSON${index}`}>
                          {parse(step.Json)}
                        </div>
                      </div>
                    )}
                  </div>
                );
              })}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

Loan.propTypes = {
  ind: PropTypes.number.isRequired,
};
