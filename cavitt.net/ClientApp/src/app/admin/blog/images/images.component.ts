import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../../services';
import { BlogImage } from '../../../models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css']
})
export class ImagesComponent implements OnInit {

  formGroup = this.fb.group({
    file: [null, Validators.required]
  });


  constructor(private blogService: BlogService, private fb: FormBuilder) { }

  selectedFile: File;
  formData: FormData;

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
    console.log(this.selectedFile);
    this.uploadFile(); 
    //console.log(event);
  }

  uploadFile() {
    this.formData = new FormData();
    this.formData.append('image', this.selectedFile, this.selectedFile.name);
    //let blogImage = new BlogImage {
    //  Image: this.selectedFile
    //}
    //// call service.
    this.blogService.uploadIamge(this.formData);
  }

  ngOnInit() {
  }

}
