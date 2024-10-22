import { Component, OnInit } from '@angular/core';
import { EncodingService } from '../encoding.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-encoding',
  templateUrl: './encoding.component.html',
  styleUrls: ['./encoding.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class EncodingComponent implements OnInit {
  encodings: string[] = [];
  selectedEncoding: string = '';
  inputText: string = '';
  resultText: string = '';

  constructor(private encodingService: EncodingService) {}

  ngOnInit(): void {
    this.encodingService.getSupportedEncodings().subscribe(
      (data) => {
        this.encodings = data;
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
        console.log('Copied to clipboard');
      },
      (err) => {
        console.error('Could not copy text: ', err);
      }
    );
  }

}
