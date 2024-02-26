import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const baseQuery = fetchBaseQuery({
  prepareHeaders: (headers) => {
    headers.set('Accept', 'application/json');
    headers.set('Content-Type', 'application/json');
  },
});

export const api = createApi({
  baseQuery,
  endpoints: (builder) => ({
    login: builder.query({
      query: () => '/api/account/login',
    }),
    isAuthenticated: builder.query({
      query: () => '/api/account/isauthenticated',
    }),
    embedForm: builder.query({
      query: (type) => `/api/form/embedform?loantype=${type}`,
    }),
    updateStatus: builder.mutation({
      query: (payload) => ({
        url: '/api/form/update/status',
        method: 'POST',
        body: payload,
      }),
    }),
    getStatuses: builder.query({
      query: () => '/api/status/all',
    }),
  }),
});

export const {
  useLazyLoginQuery,
  useLazyIsAuthenticatedQuery,
  useLazyGetStatusesQuery,
  useEmbedFormQuery,
  useUpdateStatusMutation,
  useGetCurrentEnvelopeQuery
} = api;
