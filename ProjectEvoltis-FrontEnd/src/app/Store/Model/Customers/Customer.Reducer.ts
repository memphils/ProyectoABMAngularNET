import { createReducer, on } from '@ngrx/store';
import {
    addcustomersuccess,
    deletecustomersuccess,
    getcustomersuccess,
    loadcustomer,
    loadcustomerfail,
    loadcustomersuccess,
    loadfiltercustomersuccess,
    updatecustomersuccess,
} from './Customer.Actions';
import { customerInitialState } from './Customer.State';

const _CustomerReducer = createReducer(
    customerInitialState,
    on(loadcustomer, (state) => ({ ...state, loading: true })),
    on(loadcustomersuccess, (state, action) => {
        return {
            ...state,
            loading: false,
            loaded: true,
            list: [...action.customers],
            errormessage: '',
        };
    }),
    on(loadcustomerfail, (state, action) => {
        return {
            ...state,
            loading: false,
            loaded: false,
            list: [],
            errormessage: action.errormessage,
        };
    }),
    on(getcustomersuccess, (state, action) => {
        return {
            ...state,
            loading: false,
            loaded: true,
            customerobj: action.customer,
            errormessage: '',
        }
    }),
    on(addcustomersuccess, (state, action) => {
        const _maxid = Math.max(...state.list.map(o => o.customerId));
        const _newdata = { ...action.inputdata };
        _newdata.customerId = _maxid + 1;
        return {
            ...state,
            loading: false,
            loaded: true,
            list: [...state.list, _newdata],
            errormessage: ''
        }
    }),
    on(updatecustomersuccess, (state, action) => {
        const _newdata = state.list.map(o => {
            return o.customerId === action.inputdata.customerId ? action.inputdata : o
        })
        return {
            ...state,
            loading: false,
            loaded: true,
            list: _newdata,
            errormessage: ''
        }
    }),
    on(deletecustomersuccess, (state, action) => {
        const _newdata = state.list.filter(o => o.customerId !== action.code);
        return {
            ...state,
            loading: false,
            loaded: true,
            list: _newdata,
            errormessage: ''
        }
    }),
    on(loadfiltercustomersuccess, (state, action) => {
        return {
            ...state,
            loading: false,
            loaded: true,
            list: [...action.customers],
            errormessage: '',
        };
    }),
);

export function CustomerReducer(state: any, action: any) {
    return _CustomerReducer(state, action);
}
