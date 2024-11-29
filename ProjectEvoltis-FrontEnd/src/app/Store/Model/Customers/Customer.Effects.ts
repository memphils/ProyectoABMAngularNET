import { Injectable } from "@angular/core"
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchError, exhaustMap, of, map, switchMap, tap } from "rxjs";
import { CustomerService } from "../../../Services/customer.service";
import { addcustomer, addcustomersuccess, deletecustomer, deletecustomersuccess, getcustomeraction, getcustomersuccess, loadcustomer, loadcustomerfail, loadcustomersuccess, loadfiltercustomer, loadfiltercustomersuccess, updatecustomer, updatecustomersuccess } from "./Customer.Actions";
import { showalert } from "../../Common/App.Action";
import { Router } from "@angular/router";

@Injectable()
export class CustomerEffects {
    constructor(private action$: Actions, private service: CustomerService, private router: Router) { }
    _loadcustomer = createEffect(() =>
        this.action$.pipe(
            ofType(loadcustomer),
            exhaustMap((action) => {
                return this.service.getAll().pipe(
                    map((data) => {
                        return loadcustomersuccess({ customers: data.data })
                    }),
                    catchError((_error) => of(loadcustomerfail({ errormessage: _error.message })))
                )
            })
        )
    )
    _getcustomer = createEffect(() =>
        this.action$.pipe(
            ofType(getcustomeraction),
            exhaustMap((action) => {
                return this.service.getById(action.id).pipe(
                    map((data) => {
                        return getcustomersuccess({ customer: data.data })
                    }),
                    catchError((_error) => of(showalert({ message: 'Fallo obtener cliente :' + _error.message, resulttype: 'fail' })))
                )
            })
        )
    )

    _addcustomer = createEffect(() =>
        this.action$.pipe(
            ofType(addcustomer),
            switchMap((action) => {
                return this.service.postCustomer(action.inputdata).pipe(
                    switchMap((data) => {
                        return of(addcustomersuccess({ inputdata: action.inputdata }),
                            showalert({ message: data.message, resulttype: 'pass' }))
                    }),
                    catchError((_error) => of(showalert({ message: 'Fallo el creacion de cliente', resulttype: 'fail' })))
                )
            })
        )
    )
    _updatecustomer = createEffect(() =>
        this.action$.pipe(
            ofType(updatecustomer),
            switchMap((action) => {
                return this.service.putCustomer(action.inputdata).pipe(
                    switchMap((data) => {
                        return of(updatecustomersuccess({ inputdata: action.inputdata }),
                            showalert({ message: data.message, resulttype: 'pass' }))
                    }),
                    catchError((_error) => of(showalert({ message: 'Fallo actualizacion de cliente', resulttype: 'fail' })))
                )
            })
        )
    )
    _deletecustomer = createEffect(() =>
        this.action$.pipe(
            ofType(deletecustomer),
            switchMap((action) => {
                return this.service.deleteById(action.code).pipe(
                    switchMap((data) => {
                        return of(deletecustomersuccess({ code: action.code }),
                            showalert({ message: data.message, resulttype: 'pass' }))
                    }),
                    catchError((_error) => of(showalert({ message: 'Fallo el borrado de cliente', resulttype: 'fail' })))
                )
            })
        ))

    _getfilterssociate = createEffect(() =>
        this.action$.pipe(
            ofType(loadfiltercustomer),
            switchMap((action) => {
                return this.service.getAllByCriteria(action.inputdata).pipe(
                    switchMap((data) => {
                        return of(loadfiltercustomersuccess({ customers: data.data }),
                            showalert({ message: data.message, resulttype: 'pass' }))
                    }),
                    catchError((_error) => of(showalert({ message: 'Fallo obtener datos por cryteria', resulttype: 'fail' })))
                )
            })
        ))
    _navigateOnSuccess$ = createEffect(
        () =>
            this.action$.pipe(
                ofType(addcustomersuccess, updatecustomersuccess),
                tap(() => {
                    this.router.navigate(['/']);
                })
            ),
        { dispatch: false }
    );
}