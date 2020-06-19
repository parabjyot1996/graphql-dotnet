import { NgModule } from '@angular/core';
import { RouterModule, Routes } from "@angular/router"
import { HomeComponent } from '../home/home.component';
import { CreateOwnerComponent } from '../create-owner/create-owner.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: "home",
    component: HomeComponent
  },
  {
    path: "createowner",
    component: CreateOwnerComponent
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
