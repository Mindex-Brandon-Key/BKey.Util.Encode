import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { EncodingService } from './encoding.service';



@Component({
  selector: 'app-encoding',
  templateUrl: './encoding.component.html',
  styleUrls: ['./encoding.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule
  ]
})
export class EncodingComponent implements OnInit {
  encodings: string[] = [];
  selectedEncoding: string = '';
  inputText: string = '';
  resultText: string = '';
  showCopyAlert: boolean = false;

  constructor(private encodingService: EncodingService) {
  }

  ngOnInit() {
    this.encodingService.getSupportedEncodings()
      .subscribe(
        (encodings) => {
          this.encodings = encodings;
          if (this.encodings.length === 0) {
            this.selectedEncoding = 'No Encodings Available';
          }
        },
        (error) => {
          console.error('Failed to fetch supported encodings:', error);
          this.encodings = [];
          this.selectedEncoding = 'No Encodings Available';
        }
      );
  }

  onEncode(): void {
    if (this.selectedEncoding && this.inputText) {
      this.encodingService.encodeText(this.selectedEncoding, this.inputText).subscribe(
        (result) => {
          this.resultText = result;
        },
        (error) => {
          this.resultText = 'Error: ' + error.error;
        }
      );
    }
  }

  copyToClipboard(): void {
    navigator.clipboard.writeText(this.resultText).then(
      () => {
        this.showCopyAlert = true;
        setTimeout(() => {
          this.showCopyAlert = false;
        }, 2000);
      },
      (err) => {
        console.error('Could not copy text: ', err);
      }
    );
  }

}
