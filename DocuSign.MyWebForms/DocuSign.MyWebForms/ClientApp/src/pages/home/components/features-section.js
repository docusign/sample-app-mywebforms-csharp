import React from 'react';
import { useTranslation } from 'react-i18next';
import { FeatureCard } from './feature-card';

export const FeaturesSection = () => {
  const { t } = useTranslation('Cards');
  const cards = t('Cards', { returnObjects: true });

  return (
    <div className='row'>
      {cards.map((card, index) => {
        return <FeatureCard data={card} key={card.Title} index={index} />;
      })}
    </div>
  );
};
