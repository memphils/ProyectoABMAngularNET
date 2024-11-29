import { Customer } from "../Customer";

export interface CustomerState{
    loading:boolean,
    loaded:boolean,
    list:Customer[],
    customerobj:Customer,
    errormessage:string
}

export const customerInitialState:CustomerState={
    loading:false,
    loaded:false,
    list:[],
    errormessage:'',
    customerobj:{
        customerId: 0,
        customerName: "",
        contactName: "",
        address: "",
        city: ""
    }
}