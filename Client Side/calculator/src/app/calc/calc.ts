import { Component, inject, signal } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import {MatListModule} from '@angular/material/list';

import { MatFormFieldModule } from '@angular/material/form-field';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { response } from 'express';
import { history, operation} from './operation.model'
import { Sign } from 'crypto';
@Component({
  imports: [FormsModule, MatSelectModule,
    MatFormFieldModule, MatListModule],
  selector: 'app-calc',
  styleUrl: './calc.css',
  templateUrl: './calc.html',
})
export class Calc {
  private http = inject(HttpClient);

  number1 = 0;
  number2 = 0;
  operator = "";
  options = [{symbol: '+', op:'Add'},{symbol: '-', op:'Subtract'},{symbol: '*', op:'Multiply'},{symbol: '/', op:'Divide'} ];
  result = signal<Number>(0);

  selectedOption = '';
  Last3OfType = signal<operation[]>([])
  LastMonthOfType = signal<operation[]>([])
  onSubmit(): void {
    const body = {
      number1: this.number1,
      number2: this.number2,
      operationType: this.selectedOption,
    };
    this.http.post<Number>('https://localhost:7245/Calculate/Calculate', body).subscribe({
      next: (response) => {
        this.result.set(response);
      },
      error: (error) => {
        console.error('Request failed:', error);
      },
    })

    this.http.get<history>('https://localhost:7245/Calculate/History/'+this.selectedOption).subscribe({
      next: (response) => {
        this.Last3OfType.set(response.last3OfType)
        this.LastMonthOfType.set(response.lastMonthOfType)
      }
    })
  }
operandToSymbol(op:string){
  switch(op){
    case "Add":
      return "+"
    case "Subtract":
      return "-";
    case "Multiply":
      return "*";
  }
  return "/";
}

}