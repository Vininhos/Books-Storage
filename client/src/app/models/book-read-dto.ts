export class BookReadDto {
  public id: string;
  public name: string;
  public author: string;
  public publicationYear: number;
  public price: number;
  public category: string;
  public email: string;

  constructor(
    id: string,
    name: string,
    author: string,
    publicationYear: number,
    price: number,
    category: string,
    email: string
  ) {
    this.id = id;
    this.name = name;
    this.author = author;
    this.publicationYear = publicationYear;
    this.price = price;
    this.category = category;
    this.email = email;
  }
}
