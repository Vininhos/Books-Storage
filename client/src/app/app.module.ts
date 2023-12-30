import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from '@angular/common/http';
import { BookFormComponent } from './book-form/book-form.component';
import { BookComponent } from './book/book.component';
import { provideRouter } from '@angular/router';
import {routes} from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { BookHomeComponent } from './book-home/book-home.component';

@NgModule({
  declarations: [
    AppComponent,
    BookFormComponent,
    BookComponent,
    BookHomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [provideRouter(routes)],
  bootstrap: [AppComponent]
})
export class AppModule { }
