# html++ (A more stupid way of writing ```html```)

> ## ```html++``` is a more ```advancedly```-(stupid) way of writing html.

# Example
```csharp
html {
    head {
        title { My Page }     
    }
    body {
        h1 { Welcome to the website }
        
        @for i:int in 1..3 {
            p { This is paragraph number $i }
        }
        
        @while i < 5 {
            p { Another paragraph, $i }
            i += 1
        }
        
        @if i >= 4 {
            div { This is a dir inside an if block }
        } else if i == 3 {
            div { This is a dir inside an else-if block }
        } else {
            div { This is a dir inside an else block }
        }
        
        script() {
            console.log("This is a script block");
            console.log(document)
        }

        style() {
            body {
                background:white;
                padding:200px; 
                font-family:lucida console;
            },

            h1 {
                color:dodgerblue;
                font-family:monospace;
                font-size:1145px;
            }
        }
    }
}

```

> ### HELPFULL NOTE: some of the functions do not work such as ```@for , @if and all of the other ones!```, Enjoy!

- ## Was generally made because: ```I was bored```
- ## Author: ```hmZa-Sfyn```(Me)
- ## Date of publish: ```5/11/2024```