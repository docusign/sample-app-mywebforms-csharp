import React from 'react';
import { Loan } from '../../components';
import { loanTypes } from '../../constants';

const { personal } = loanTypes;

export const PersonalLoan = () => {
  return <Loan ind={personal} />;
};
