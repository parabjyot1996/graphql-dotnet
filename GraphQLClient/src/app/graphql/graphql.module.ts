import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

//Apollo
import { ApolloModule, Apollo } from 'apollo-angular';
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http';

@NgModule({
  exports: [
    ApolloModule,
    HttpLinkModule,
    HttpClientModule
  ]
})
export class GraphqlModule {

  constructor() {
    
  }
}