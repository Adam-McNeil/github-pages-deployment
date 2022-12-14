mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  MakeJiraRequestJavaScript: function(url, encodedStr) {
    window.alert("Started")
    const userAction = async () => {
      const response = await fetch(url​, {
        method: 'GET',
        body: myBody, // string or object
        headers: {
          'Content-Type': 'application/json'
          'Authorization': 'Basic ' + encodedStr
        }
      });
      const myJson = await response.json(); //extract JSON from the http response
      // do something with myJson
      window.alert(myJson)
    }
  }

  HelloString: function (str) {
    window.alert(UTF8ToString(str));
  },

  PrintFloatArray: function (array, size) {p
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});


















mergeInto(LibraryManager.library, {

    Hello: function () {
        window.alert("Hello, world!");
    },

    MakeJiraRequestJavaScript: function (url, encodedStr)
    {
        window.alert("Started")
        const userAction = async () =>
        {
            const response = await fetch(url,
            {
                method: 'GET',
                body: myBody, // string or object
                headers:
                {
                    'Content-Type': 'application/json'
                    'Authorization': 'Basic ' + encodedStr
                }
            });
        }
        const myJson = await response.json(); //extract JSON from the http response
        // do something with myJson
        window.alert(myJson)
    }
});