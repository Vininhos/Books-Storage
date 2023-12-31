import { Component, inject, Input } from "@angular/core";
import { BookService } from "../services/book.service";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { BookCreateDto } from "../models/book-create-dto";
import { BookReadDto } from "../models/book-read-dto";

@Component({
  selector: "app-book-form",
  templateUrl: "./book-form.component.html",
  styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent {
  bookService: BookService = inject(BookService);

  bookForm = new FormGroup({
    name: new FormControl(""),
    author: new FormControl(""),
    publicationYear: new FormControl(""),
    price: new FormControl(""),
    category: new FormControl(""),
    email: new FormControl(""),
  }) as FormGroup & { value: BookCreateDto };

  newBook: BookCreateDto = new BookCreateDto();

  constructor() { }

  submitBook() {
    // if (this.bookForm.invalid) {
    //   return;
    // }

    this.newBook = this.bookForm.value;

    this.bookService.insertBook(this.newBook);
  }

}
