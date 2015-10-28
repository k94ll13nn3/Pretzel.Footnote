# Pretzel.Footnote
A footnote tag for Pretzel

This is a plugin for the static site generation tool [Pretzel](https://github.com/Code52/pretzel).

Since the markdown engine used by Pretzel does not render footnotes, this plugin aims at performing this task.

### Usage

To create a footnote :
```
{% footnote string %}
```

To reference an existing footnote, you can use :
```
{% footnote_id id %}
```
where id is the number used to reference the wanted footnote.

To render the footnote list :
```
{% footnote_render %}
```

The rendered output will be an ordered list (`<ol></ol>`) in a `<div class="footnotes"></div>`.

#### Warnings

- The `footnote_render` must be used in the same file as the `footnote`.
- Using `footnote_render` will clear all the stored footnotes so you can only use it once per file.
- Do not write the tag on multiple line, as it will not be correctly rendered by the `post.excerpt` (either a Pretzel bug or a bug in this plugin).

### Installation

Compile the solution `Pretzel.Footnote.sln` and copy `Pretzel.Footnote.dll` to the `_plugins` folder at the root of your site folder (VS2015 needed).