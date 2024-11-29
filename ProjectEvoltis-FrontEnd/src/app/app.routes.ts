import { Routes } from '@angular/router';
import { InicioComponent } from './Pages/inicio/inicio.component';
import { CustomerComponent } from './Pages/customer/customer.component';

export const routes: Routes = [
    {path:'',component:InicioComponent},
    {path:"inicio",component:InicioComponent},
    {path:"customer/:id",component:CustomerComponent},
];
