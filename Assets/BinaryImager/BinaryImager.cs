//
// BinaryImager - 1-bit monochrome image effect
//
// Copyright (C) 2015 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using UnityEngine;

[ExecuteInEditMode]
public class BinaryImager : MonoBehaviour
{
    #region Public Properties

    // Dither matrix selector

    public enum DitherMatrix {
        Bayer2x2, Bayer3x3, Bayer4x4, Bayer8x8
    };

    [SerializeField]
    DitherMatrix _ditherMatrix;

    public DitherMatrix ditherMatrix {
        get { return _ditherMatrix; }
        set { _ditherMatrix = value; }
    }

    // Scale factor for dither matrix

    [SerializeField, Range(1, 8)]
    int _ditherScale = 1;

    public int ditherScale {
        get { return _ditherScale; }
        set { _ditherScale = value; }
    }

    // Color (dark)

    [SerializeField]
    Color _color0 = Color.black;

    public Color color0 {
        get { return _color0; }
        set { _color0 = value; }
    }

    // Color (light)

    [SerializeField]
    Color _color1 = Color.white;

    public Color color1 {
        get { return _color1; }
        set { _color1 = value; }
    }

    #endregion

    #region Private Resources

    [SerializeField] Shader _shader;
    [SerializeField] Texture2D _ditherTexture2x2;
    [SerializeField] Texture2D _ditherTexture3x3;
    [SerializeField] Texture2D _ditherTexture4x4;
    [SerializeField] Texture2D _ditherTexture8x8;

    Texture2D DitherMatrixTexture {
        get {
            switch (_ditherMatrix) {
                case DitherMatrix.Bayer2x2: return _ditherTexture2x2;
                case DitherMatrix.Bayer3x3: return _ditherTexture3x3;
                case DitherMatrix.Bayer4x4: return _ditherTexture4x4;
                default: return _ditherTexture8x8;
            }
        }
    }

    Material _material;

    #endregion

    #region MonoBehaviour Functions

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null) {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        _material.SetTexture("_DitherTex", DitherMatrixTexture);
        _material.SetFloat("_DitherScale", _ditherScale);
        _material.SetColor("_Color0", _color0);
        _material.SetColor("_Color1", _color1);

        Graphics.Blit(source, destination, _material);
    }

    #endregion
}
