'use strict';

var gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglifyCss = require('gulp-uglifycss'),
    del = require('del')
    
gulp.task('clean', function () {
    return del(['wwwroot/lib'])
});

gulp.task('copyJavaScript', function () {

    gulp.src(['bower_components/jquery/dist/jquery.min.js',
              'bower_components/bootstrap/dist/js/bootstrap.min.js',
              'bower_components/angular/angular.min.js'])
        .pipe(concat('third-party.js'))
        .pipe(gulp.dest('wwwroot/lib'))
});

//gulp.task('sass', function () {

//    return gulp.src('bower_components/*.scss')
//    .pipe(sass())
//    .pipe(gulp.dest('lib'))
//});

gulp.task('copyCss', function () {

    gulp.src(['bower_components/bootstrap/dist/css/bootstrap.min.css', 'Assets/main.css'])
        .pipe(uglifyCss())
        .pipe(concat('site.css'))
        .pipe(gulp.dest('wwwroot/lib'))
});

gulp.task('default', ['clean', 'copyJavaScript', 'copyCss']);