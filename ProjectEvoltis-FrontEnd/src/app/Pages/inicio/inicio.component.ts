import { Component, inject, ViewChild } from '@angular/core';

import {MatCardModule} from '@angular/material/card';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

import { Router } from '@angular/router';
import { Customer } from '../../Store/Model/Customer';
import { Store } from '@ngrx/store';
import { deletecustomer, loadcustomer, loadfiltercustomer } from '../../Store/Model/Customers/Customer.Actions';
import { AppState } from '../../Store/app.reducers';
import { getcustomerbycryteria, getcustomerlist } from '../../Store/Model/Customers/Customer.Selector';
import { MatPaginator } from "@angular/material/paginator"
import { MatSort } from "@angular/material/sort"
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [FormsModule, MatInputModule,MatFormFieldModule,MatPaginator,MatCardModule,MatTableModule,MatIconModule,MatButtonModule],
  templateUrl: './inicio.component.html',
  styleUrl: './inicio.component.css'
})
export class InicioComponent {
  datasource: any;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  private store = inject(Store<AppState>);
  private router= inject(Router);
  public searchQuery:string='';
  public listaCustomers:Customer[] = [];
  public displayedColumns : string[] = ['customerName','contactName','address','city','accion'];

  ngOnInit(): void {
    this.getCustomers();
  }  

  nuevo(){
    this.router.navigate(['/customer/0']);
  }

  editar(objeto:Customer){
   // this.store.dispatch(getcustomer({id:objeto.customerId}))
    this.router.navigate(['/customer/'+objeto.customerId]);
  }

  eliminar(code:number){
    Swal.fire({
      title: "¿Esta seguro?",
      text: "Esto eliminara al empleado de manera definitiva!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Si"
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: "Eliminado!",
          text: "El empleado ha sido eliminado.",
          icon: "success"
        });
        this.store.dispatch(deletecustomer({code:code}));
      }
    });
   /* if(confirm('¿Desea eliminar al cliente?')){
      this.store.dispatch(deletecustomer({code:code}));
    }*/
  }

  search(){
    if(this.searchQuery!='')
    {
      this.store.dispatch(loadfiltercustomer({inputdata:this.searchQuery}));
    this.store.select(getcustomerbycryteria).subscribe(item => {
      this.listaCustomers = item;
      this.datasource = new MatTableDataSource<Customer>(this.listaCustomers);
      this.datasource.paginator = this.paginator;
      this.datasource.sort = this.sort;
    });
    }
    else{
      this.getCustomers();
    }
  }

  getCustomers(){
    this.store.dispatch(loadcustomer());
    this.store.select(getcustomerlist).subscribe(item => {
      this.listaCustomers = item;
      this.datasource = new MatTableDataSource<Customer>(this.listaCustomers);
      this.datasource.paginator = this.paginator;
      this.datasource.sort = this.sort;
    });
  }
}