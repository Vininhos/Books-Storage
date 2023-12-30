import { Routes } from "@angular/router";
import { BookFormComponent } from "./book-form/book-form.component";
import { BookHomeComponent } from "./book-home/book-home.component";

export const routes = [
    { path: 'book-form', component: BookFormComponent },
    { path: 'home', component: BookHomeComponent }
];
