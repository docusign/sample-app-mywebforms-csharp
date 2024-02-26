import React from 'react';
import { Loan } from '../../components';
import { loanTypes } from '../../constants';

const { sailboat } = loanTypes;

export const SailboatLoan = () => {
  return <Loan ind={sailboat} />;
};
