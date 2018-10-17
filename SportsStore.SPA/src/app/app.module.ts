import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';

import { ModelModule } from './models/model.module';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    ModelModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [ModelModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
