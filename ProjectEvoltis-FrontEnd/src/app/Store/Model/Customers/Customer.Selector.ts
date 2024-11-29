import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CustomerState } from "./Customer.State";

const getcustomerstate = createFeatureSelector<CustomerState>('customers');

export const getcustomerlist = createSelector(getcustomerstate, (state) => {
    return state.list;
})
export const getcustomerbycryteria = createSelector(getcustomerstate, (state) => {
    return state.list;
})

export const getcustomer = createSelector(getcustomerstate, (state) => {
    return state.customerobj;
})