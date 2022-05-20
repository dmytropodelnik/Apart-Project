import { Component, OnInit } from '@angular/core';
import { MailLetter } from 'src/app/models/mailletter.item';

import AuthHelper from '../../utils/authHelper';

@Component({
  selector: 'app-letter-creator',
  templateUrl: './letter-creator.component.html',
  styleUrls: ['./letter-creator.component.css']
})
export class LetterCreatorComponent implements OnInit {
  sentLetters: MailLetter[] = [];

  newLetter: MailLetter = new MailLetter();

  choice: boolean = false;

  htmlLetterFile: File | null = null;

  constructor() { }

  setChoice(): void {
    this.choice = !this.choice;
  }

  createLetter(): void {
    let letter;

    if (this.choice) {
      if (this.htmlLetterFile == null) {
        alert("Upload a file!");
        return;
      }
      // checking file type
      if (!['text/html', 'text/plain'].includes(this.htmlLetterFile.type)) {
        alert('Only HTML files are allowed!');
        return;
      }
      this.uploadFile();

      letter = {
        title: this.newLetter.title,
        text: this.newLetter.text,
        sender: AuthHelper.getLogin(),
      };

      fetch('https://localhost:44381/api/deals/sendbestdealsletter', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      })
      .then(r => r.json())
      .then(r => {
        if (r.code === 200) {
          alert("File has been successfully uploaded!");
        } else {
          alert("Uploading file error!");
        }
      })
      .catch(err => {
        alert(err);
      });
    } else {

    }
  }

  async uploadFile(): Promise<void> {
    let fData = new FormData();
    if (this.htmlLetterFile != null) {
      fData.append('uploadedFile', this.htmlLetterFile);
    }

      await fetch('https://localhost:44381/api/fileuploader/uploadfile', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: fData,
      })
      .then(r => r.json())
      .then(r => {
        if (r.code === 200) {
          alert("File has been successfully uploaded!");
        } else {
          alert("Uploading file error!");
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  handleFileInput(files: FileList): void {
    if (files !== null) {
      this.htmlLetterFile = files.item(0);
    }
  }

  ngOnInit(): void {
  }

}
