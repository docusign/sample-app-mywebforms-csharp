import React from 'react';
import { Loan } from '../../components';
import { loanTypes } from '../../constants';

const { auto } = loanTypes;

export const AutoLoan = () => {
  return <Loan ind={auto} />;
};
