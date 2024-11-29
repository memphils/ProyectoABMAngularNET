import { Component, Input, OnInit, inject } from '@angular/core';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Customer } from '../../Store/Model/Customer';
import { Store } from '@ngrx/store';
import { AppState } from '../../Store/app.reducers';
import { getcustomer } from '../../Store/Model/Customers/Customer.Selector';
import { addcustomer, updatecustomer,getcustomeraction } from '../../Store/Model/Customers/Customer.Actions';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
  @Input('id') idCustomer : number=0;
  public title:string='';
  private store = inject(Store<AppState>);
  private customer: any;
  public formBuild = inject(FormBuilder);
  public formCustomer: FormGroup = this.formBuild.group({
    customerName: [''],
    contactName: [''],
    address: [''],
    city: ['']
  });
  constructor(private router: Router) { }

  ngOnInit(): void {
    if(this.idCustomer!=0){
    this.store.dispatch(getcustomeraction({id:this.idCustomer}))
    this.store.select(getcustomer).subscribe(data => {
      this.customer = data;
      this.formCustomer.patchValue({
        customerName: data.customerName,
        contactName: data.contactName,
        address: data.address,
        city: data.city
      })
      if(this.customer.customerId ==0) 
        this.title="Nuevo Empleado";
      else
      this.title = "Editar datos de: "+this.customer.customerName;
    })}
  }

  guardar() {
    const objeto: Customer = {
      customerId: this.idCustomer,
      customerName: this.formCustomer.value.customerName,
      contactName: this.formCustomer.value.contactName,
      address: this.formCustomer.value.address,
      city: this.formCustomer.value.city,
    };
    debugger
    if (this.idCustomer ==0) {
      this.store.dispatch(addcustomer({ inputdata: objeto }))
    } else {
      this.store.dispatch(updatecustomer({ inputdata: objeto }))
    }
  }

  volver() {
    this.router.navigate(["/"]);
  }
}
