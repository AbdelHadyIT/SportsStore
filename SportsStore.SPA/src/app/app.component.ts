import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Repository } from './models/repository';
import { Product } from './models/product.model';
import { Supplier } from './models/supplier.model';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor (private repo: Repository) {}

  get product (): Product {
    return this.repo.product;
  }

  get products (): Product[] {
    return this.repo.products;
  }

  createProduct() {
    this.repo.CreateProduct(new Product(0, 'X-Ray Scuba Mask', 'Watersports',
    'See what the fish are hiding', 49.99,
    this.repo.products[0].supplier ));
  }
  createProductAndSupplier() {
    const supplier = new Supplier(0, 'Rocket Shoe Crop', 'Boston', 'MA');
    const product = new Product(0, 'Rocket-Powered Shoes', 'Running',
    'Set a new record', 100, supplier);
    this.repo.createProductAndSupplier(product, supplier);
  }
}
