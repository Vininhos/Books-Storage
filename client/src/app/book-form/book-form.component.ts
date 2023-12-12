import { Component, inject, Input } from "@angular/core";
import { BookService } from "../services/book.service";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { Book } from "../models/book";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent {
  bookService: BookService = inject(BookService);

  bookForm = new FormGroup({
    bookName: new FormControl(""),
    author: new FormControl(""),
    yearOfPublication: new FormControl(""),
    price: new FormControl(""),
    category: new FormControl(""),
    email: new FormControl(""),
  });

  constructor() {}

  submitBook() {
    console.log(
      `Book test: ${this.bookForm.get('category')!.value}`
    );
  }

  getAllBooks() {
    this.bookService
      .getAllBooks()
      .then((response) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      });
  }
}
