import { ActionReducerMap} from '@ngrx/store';
import * as reducers from './Model/Customers/Customer.Reducer';
import * as state from './Model/Customers/Customer.State'

export interface AppState {
        customers:state.CustomerState
};

export const appReducers: ActionReducerMap<AppState> = {
    customers: reducers.CustomerReducer
 }