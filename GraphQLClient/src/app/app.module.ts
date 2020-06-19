import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { HomeComponent } from './home/home.component';

//Apollo
import { GraphqlModule } from './graphql/graphql.module';

//Service
import { GraphqlService } from './graphql/graphql.service';
import { CreateOwnerComponent } from './create-owner/create-owner.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CreateOwnerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    GraphqlModule
  ],
  providers: [GraphqlService],
  bootstrap: [AppComponent]
})
export class AppModule { }
