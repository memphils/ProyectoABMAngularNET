import { createAction, props } from "@ngrx/store";
import { Customer } from "../Customer";

export const LOAD_CUSTOMER='[customer page]load customer'
export const LOAD_CUSTOMER_SUCCESS='[customer page]load customer success'
export const LOAD_CUSTOMER_FAIL='[customer page]load customer fail'

export const DELETE_CUSTOMER='[customer page]delete customer'
export const DELETE_CUSTOMER_SUCCESS='[customer page]delete customer success'

export const ADD_CUSTOMER='[customer page]add customer'
export const ADD_CUSTOMER_SUCCESS='[customer page]add customer success'

export const UPDATE_CUSTOMER='[customer page]update customer'
export const UPDATE_CUSTOMER_SUCCESS='[customer page]update customer success'

export const GET_CUSTOMER='[customer page]get customer'
export const GET_CUSTOMER_SUCCESS='[customer page]get customer success'

export const LOAD_FILTER_CUSTOMER='[customer page]load filters customer'
export const LOAD_FILTER_CUSTOMER_SUCCESS='[customer page]load filters customer success'


export const loadcustomer=createAction(LOAD_CUSTOMER)
export const loadcustomersuccess=createAction(LOAD_CUSTOMER_SUCCESS,props<{customers:Customer[]}>())
export const loadcustomerfail=createAction(LOAD_CUSTOMER_FAIL,props<{errormessage:string}>())

export const addcustomer=createAction(ADD_CUSTOMER,props<{inputdata:Customer}>())
export const addcustomersuccess=createAction(ADD_CUSTOMER_SUCCESS,props<{inputdata:Customer}>())

export const updatecustomer=createAction(UPDATE_CUSTOMER,props<{inputdata:Customer}>())
export const updatecustomersuccess=createAction(UPDATE_CUSTOMER_SUCCESS,props<{inputdata:Customer}>())

export const deletecustomer=createAction(DELETE_CUSTOMER,props<{code:number}>())
export const deletecustomersuccess=createAction(DELETE_CUSTOMER_SUCCESS,props<{code:number}>())

export const getcustomeraction=createAction(GET_CUSTOMER,props<{id:number}>())
export const getcustomersuccess=createAction(GET_CUSTOMER_SUCCESS,props<{customer:Customer}>())

export const loadfiltercustomer=createAction(LOAD_FILTER_CUSTOMER,props<{inputdata:string}>())
export const loadfiltercustomersuccess=createAction(LOAD_FILTER_CUSTOMER_SUCCESS,props<{customers:Customer[]}>())