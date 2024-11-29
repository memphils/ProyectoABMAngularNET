import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { Customer } from '../Store/Model/Customer';
import { Response } from '../Models/Response';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private http= inject(HttpClient);
  private apiUrl:string = appsettings.apiUrl + "Customers";
  constructor() { }

  getAll(){
    return this.http.get<Response<Customer[]>>(`${this.apiUrl}/GetAllAsync`);
  }
  getById(id:number){
    return this.http.get<Response<Customer>>(`${this.apiUrl}/GetAsync/${id}`);
  }

  postCustomer(customer:Customer){
    return this.http.post<Response<boolean>>(`${this.apiUrl}/InsertAsync`,customer);
  }
  putCustomer(customer:Customer){
    return this.http.put<Response<boolean>>(`${this.apiUrl}/UpdateAsync`,customer);
  }
  deleteById(id:number){
    return this.http.delete<Response<boolean>>(`${this.apiUrl}/DeleteAsync/${id}`);
  }
  getAllByCriteria(filter:string){
    return this.http.get<Response<Customer[]>>(`${this.apiUrl}/GetByCriteriaAsync/${filter}`);
  }
}

